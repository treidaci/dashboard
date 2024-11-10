import axios from 'axios';

const API_BASE_URL = 'http://localhost:5237/api';

export const getPlayerActivities = async (playerId) => {
    const response = await axios.get(`${API_BASE_URL}/players/${playerId}/activities`);
    return response.data;
};
