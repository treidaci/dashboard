import React, { useState } from 'react';
import './PlayerActivityCard.css';
import { updatePlayerActivityStatus } from '../../services/api';

const PlayerActivityCard = ({ playerId, id, action, timestamp, status, reason }) => {
    const [currentStatus, setCurrentStatus] = useState(status);

    const handleStatusChange = async (newStatus) => {
        try {
            await updatePlayerActivityStatus(playerId, id, newStatus, "Marked by admin");
            setCurrentStatus(newStatus);
        } catch (error) {
            console.error("Failed to update player activity status:", error);
        }
    };

    return (
        <div className={`activity-card ${currentStatus}`}>
            <h3>Action: {action}</h3>
            <p><strong>Timestamp:</strong> {new Date(timestamp).toLocaleString()}</p>
            <p><strong>Status:</strong> {currentStatus}</p>
            {reason && <p><strong>Reason:</strong> {reason}</p>}
            <div className="status-options">
                <label htmlFor="status">Mark as:</label>
                <select
                    id="status"
                    value={currentStatus}
                    onChange={(e) => handleStatusChange(e.target.value)}
                >
                    <option value="Legitimate">Legitimate</option>
                    <option value="Suspicious">Suspicious</option>
                    <option value="Malicious">Malicious</option>
                </select>
            </div>
        </div>
    );
};

export default PlayerActivityCard;
