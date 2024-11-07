
# Detection Dashboard Digital Capabilities

This table details the digital capabilities, activities, and steps involved in the Detection Dashboard based on the job stories.

---

## Digital Capability 1: Player Activity

| **Digital Capability** | **Activity**                        | **Activity Step**                   | **Participants**      | **Description**                                                                                                 |
|------------------------|-------------------------------------|-------------------------------------|------------------------|---------------------------------------------------------------------------------------------------------------|
| **Player Activity**    | View Player Activity Log           | Access Activity Log Page            | User (Moderator/Admin) | User navigates to the activity log page on the dashboard to view a list of all player actions.               |
|                        |                                     | Retrieve and Display Activities     | System                 | System retrieves player actions from the database, including details like player ID, action, and timestamp. |
|                        | Filter Activity Log                | Apply Date/Time Filter              | User                   | User selects a date/time filter to narrow down player activities within a specific period.                   |
|                        |                                     | Apply Action Type Filter            | User                   | User filters activities by action type (e.g., "move", "attack") to focus on specific actions.                |
|                        |                                     | Retrieve Filtered Results           | System                 | System updates the displayed list based on applied filters.                                                   |
|                        | Detect Suspicious Activities       | Apply Detection Logic               | System                 | System uses detection logic to flag activities based on inhuman speeds or repetitive actions.                |
|                        |                                     | Mark Activities as Suspicious       | System                 | System automatically marks flagged activities with a "suspicious" status for review.                         |
|                        | Manually Review Flagged Activities | Access Suspicious Activity List     | User                   | User navigates to a filtered view of activities flagged as suspicious.                                       |
|                        |                                     | Review Activity Details             | User                   | User clicks on a flagged activity to review detailed information.                                           |
|                        |                                     | Mark as Legitimate or Malicious     | User                   | User marks activity as legitimate or malicious based on manual review.                                       |

---

## Digital Capability 2: Player Status

| **Digital Capability** | **Activity**                  | **Activity Step**            | **Participants**      | **Description**                                                                                             |
|------------------------|-------------------------------|------------------------------|------------------------|-------------------------------------------------------------------------------------------------------------|
| **Player Status**      | View Player Status           | Access Player Status Page    | User (Moderator/Admin) | User navigates to the player status page to see each player’s current status.                               |
|                        |                               | Retrieve Player Statuses     | System                 | System retrieves the current status (active, suspicious, banned) for each player from the database.         |
|                        | Update Player Status         | Select Player for Update     | User                   | User selects a player whose status needs updating based on recent behavior.                                 |
|                        |                               | Change Status                | User                   | User selects a new status (e.g., active, suspicious, banned) from available options.                        |
|                        |                               | Save Updated Status          | System                 | System saves the new status for the selected player, updating it in the database.                           |

---

## Summary

- **Digital Capability** defines the overarching capability.
- **Activity** and **Activity Step** break down the tasks and steps needed to achieve each feature's goals.
- **Participants** specify the users or systems involved.
- **Description** provides additional context for each action.
