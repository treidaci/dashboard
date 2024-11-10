import React from 'react';
import { updatePlayerStatus } from '../../services/api';
import './PlayerStatusUpdater.css';

const PlayerStatusUpdater = ({ playerId }) => {
    const handleStatusUpdate = async (status, reason) => {
        try {
            await updatePlayerStatus(playerId, status, reason);
            alert(`Player status updated to ${status}`);
        } catch (error) {
            console.error("Failed to update player status:", error);
            alert("Failed to update player status.");
        }
    };

    return (
        <div className="player-status-updater">
            <button onClick={() => handleStatusUpdate("Suspicious", "Suspicious activities")}>
                Mark as Suspicious
            </button>
            <button onClick={() => handleStatusUpdate("Active", "Valid session")}>
                Activate
            </button>
            <button onClick={() => handleStatusUpdate("Banned", "Banned for malicious actions")}>
                Ban
            </button>
        </div>
    );
};

export default PlayerStatusUpdater;
