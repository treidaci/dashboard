// this file can be broken down into its own library - maybe choose react query
import axios from 'axios';

const API_BASE_URL = 'http://localhost:5237/api';

export const getPlayerActivities = async (playerId) => {
    const response = await axios.get(`${API_BASE_URL}/players/${playerId}/activities`);
    return response.data;
};


export const getPlayerStatuses = async () => {
    const response = await axios.get(`${API_BASE_URL}/players/statuses`);
    return response.data;
};

export const updatePlayerActivityStatus = async (playerId, activityId, status, reason) => {
    const payload = {
        id: activityId,
        status: status,
        reason: reason
    };
    
    await axios.put(`${API_BASE_URL}/players/${playerId}/activity`, payload);
};

export const updatePlayerStatus = async (playerId, status, reason) => {
    const payload = {
        status: status,
        reason: reason
    };
    
    await axios.put(`${API_BASE_URL}/players/${playerId}/status`, payload);
};