using UnityEngine;
using System.Collections.Generic;


  /// <summary>
  /// Orchestrates game conditions and their punishments.
  /// Listens to game events and triggers appropriate responses.
  /// Does NOT contain condition logic itself - that lives in individual condition classes.
  /// </summary>
  public class ConditionSystem
  {
      private readonly EventBus eventBus;
      private readonly EconomySystem economySystem;
      private readonly GameStateSystem gameStateSystem;

      private List<ConditionConfig> activeConditions = new();
      private Dictionary<string, IConditionChecker> conditionCheckers = new();

      public ConditionSystem(
          EventBus eventBus, 
          EconomySystem economySystem, 
          GameStateSystem gameStateSystem)
      {
          this.eventBus = eventBus;
          this.economySystem = economySystem;
          this.gameStateSystem = gameStateSystem;
      }

      /// <summary>
      /// Register a condition for monitoring.
      /// </summary>
      public void RegisterCondition(ConditionConfig config, IConditionChecker checker)
      {
          if (!activeConditions.Contains(config))
          {
              activeConditions.Add(config);
              conditionCheckers[config.ConditionId] = checker;

              // Initialize checker
              checker.Initialize(eventBus);

              Debug.Log($"Registered condition: {config.DisplayName}");
          }
      }

      /// <summary>
      /// Manually check a specific condition.
      /// Returns true if condition is satisfied.
      /// </summary>
      public bool CheckCondition(string conditionId)
      {
          if (!conditionCheckers.ContainsKey(conditionId))
          {
              Debug.LogWarning($"Condition not found: {conditionId}");
              return true;
          }

          return conditionCheckers[conditionId].IsSatisfied();
      }

      /// <summary>
      /// Trigger a condition violation and execute punishment.
      /// </summary>
      public void TriggerViolation(string conditionId)
      {
          var config = activeConditions.Find(c => c.ConditionId == conditionId);
          if (config == null)
          {
              Debug.LogWarning($"Cannot violate unknown condition: {conditionId}");
              return;
          }

          Debug.LogWarning($"Condition violated: {config.DisplayName}");

          // Execute punishment
          ExecutePunishment(config);

          // Emit event for UI/feedback
          eventBus.Publish(new ConditionViolatedEvent
          {
              ConditionId = conditionId,
              PunishmentType = config.Punishment.ToString()
          });
      }

      private void ExecutePunishment(ConditionConfig config)
      {
          switch (config.Punishment)
          {
              case PunishmentType.Fail:
                  // Instant game over
                  gameStateSystem.TransitionTo(GameState.Fail);
                  eventBus.Publish(new GameOverEvent
                  {
                      Victory = false,
                      Reason = config.DisplayName
                  });
                  Debug.Log($"Game Over: {config.DisplayName}");
                  break;

              case PunishmentType.Penalty:
                  // Deduct gold
                  economySystem.TrySpendGold(config.PenaltyAmount);
                  Debug.Log($"Penalty applied: -{config.PenaltyAmount} gold");
                  break;

              case PunishmentType.Fix:
                  // Requires player action to fix (handled by UI/gameplay)
                  Debug.Log($"Fix required: costs {config.PenaltyAmount} gold");
                  break;
          }
      }

      /// <summary>
      /// Check all Level-type conditions (called at level end).
      /// Returns true if all are satisfied.
      /// </summary>
      public bool CheckLevelConditions()
      {
          bool allSatisfied = true;

          foreach (var config in activeConditions)
          {
              if (config.Type == ConditionType.Level)
              {
                  if (!CheckCondition(config.ConditionId))
                  {
                      TriggerViolation(config.ConditionId);
                      allSatisfied = false;
                  }
              }
          }

          return allSatisfied;
      }

      public void Reset()
      {
          foreach (var checker in conditionCheckers.Values)
          {
              checker.Cleanup();
          }

          activeConditions.Clear();
          conditionCheckers.Clear();
      }
  }

  /// <summary>
  /// Interface for individual condition checking logic.
  /// Implement this for each specific condition type.
  /// </summary>
  public interface IConditionChecker
  {
      void Initialize(EventBus eventBus);
      bool IsSatisfied();
      void Cleanup();
  }

  // ===== EXAMPLE CONDITION CHECKER =====

  /// <summary>
  /// Example: Check if convoy has reached destination.
  /// </summary>
  public class ConvoyDestroyedChecker : IConditionChecker
  {
      private EventBus eventBus;
      private bool convoyDestroyed = false;

      public void Initialize(EventBus eventBus)
      {
          this.eventBus = eventBus;
          // Would subscribe to convoy events here
      }

      public bool IsSatisfied()
      {
          return !convoyDestroyed;
      }

      public void Cleanup()
      {
          // Unsubscribe from events
      }
  }