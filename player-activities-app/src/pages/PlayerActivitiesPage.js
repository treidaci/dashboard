import React, { useEffect, useState } from 'react';
import PlayerActivitiesList from '../components/PlayerActivitiesList';
import Loader from '../components/Loader';
import { getPlayerActivities } from '../services/api';

const PlayerActivitiesPage = () => {
    const [activities, setActivities] = useState([]);
    const [loading, setLoading] = useState(true);
    const playerId = 'Player123';

    useEffect(() => {
        const fetchActivities = async () => {
            try {
                const data = await getPlayerActivities(playerId);
                setActivities(data.activities);
            } catch (error) {
                console.error('Error fetching player activities:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchActivities();
    }, []);

    return (
        <div className="player-activities-page">
            <h1>Player Activities</h1>
            {loading ? <Loader /> : <PlayerActivitiesList activities={activities} />}
        </div>
    );
};

export default PlayerActivitiesPage;
