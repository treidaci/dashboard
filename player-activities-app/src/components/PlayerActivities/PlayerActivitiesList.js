import React from 'react';
import PlayerActivityCard from './PlayerActivityCard';

const PlayerActivitiesList = ({ activities, playerId }) => (
    <div className="activities-list">
        {activities.map((activity) => (
            <PlayerActivityCard key={activity.id} {...activity} playerId={playerId}/>
        ))}
    </div>
);

export default PlayerActivitiesList;
