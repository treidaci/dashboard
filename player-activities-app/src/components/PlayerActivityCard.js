import React from 'react';
import './PlayerActivityCard.css';

const PlayerActivityCard = ({ action, timestamp, status, reason }) => (
    <div className="activity-card">
        <h3>Action: {action}</h3>
        <p><strong>Timestamp:</strong> {new Date(timestamp).toLocaleString()}</p>
        <p><strong>Status:</strong> {status}</p>
        {reason && <p><strong>Reason:</strong> {reason}</p>}
    </div>
);

export default PlayerActivityCard;
