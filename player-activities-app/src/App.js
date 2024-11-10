import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import PlayerStatusTable from './components/PlayerStatuses/PlayerStatusTable';
import PlayerActivitiesPage from './pages/PlayerActivitiesPage';
import './App.css';

const App = () => (
    <Router>
        <div className="App">
            <Routes>
                <Route path="/" element={<PlayerStatusTable />} />
                <Route path="/players/:playerId/activities" element={<PlayerActivitiesPage />} />
            </Routes>
        </div>
    </Router>
);

export default App;
