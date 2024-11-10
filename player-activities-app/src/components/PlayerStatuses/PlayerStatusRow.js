import React from 'react';
import { Link } from 'react-router-dom';
import './PlayerStatusRow.css';

const PlayerStatusRow = ({ status, isReviewNeeded }) => (
    <tr className={isReviewNeeded ? 'review-needed' : ''}>
        <td>{status.playerId}
        {isReviewNeeded && <span className="review-icon"> - review needed</span>}
        </td>
        <td>{status.status}</td>
        <td>{status.reason}</td>
        <td>
            {status.playerId === 'Player123' && (
                <Link to={`/players/${status.playerId}/activities`} className="activity-link">
                    View Activities
                </Link>
            )}
        </td>
    </tr>
);

export default PlayerStatusRow;
