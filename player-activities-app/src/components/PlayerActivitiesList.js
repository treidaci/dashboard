import React from 'react';
import PlayerActivityCard from './PlayerActivityCard';

const PlayerActivitiesList = ({ activities }) => (
    <div className="activities-list">
        {activities.map((activity) => (
            <PlayerActivityCard key={activity.id} {...activity} />
        ))}
    </div>
);

export default PlayerActivitiesList;
