import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getPlayerActivities } from '../services/api';
import PlayerActivitiesList from '../components/PlayerActivities/PlayerActivitiesList';
import PlayerStatusUpdater from '../components/PlayerStatuses/PlayerStatusUpdater';
import Loader from '../components/Loader';
import './PlayerActivitiesPage.css';

const PlayerActivitiesPage = () => {
    const { playerId } = useParams();
    const [activities, setActivities] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchActivities = async () => {
            try {
                const data = await getPlayerActivities(playerId);
                setActivities(data.activities.sort((a, b) => new Date(b.timestamp) - new Date(a.timestamp)));
            } catch (error) {
                console.error('Error fetching player activities:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchActivities();
    }, [playerId]);

    return (
        <div>
            <div className="header-container">
            <h1>Activities for Player: {playerId}</h1>
            {loading ? <Loader /> : <PlayerStatusUpdater playerId={playerId} />}
            </div>            
            {loading ? <Loader /> : <PlayerActivitiesList activities={activities} playerId={playerId} />}
        </div>
    );
};

export default PlayerActivitiesPage;
