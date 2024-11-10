import React, { useEffect, useState } from 'react';
import PlayerStatusRow from './PlayerStatusRow';
import Pagination from '../Pagination';
import { getPlayerStatuses } from '../../services/api';

const PlayerStatusTable = () => {
    const [statuses, setStatuses] = useState([]);
    const [filteredStatuses, setFilteredStatuses] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [searchTerm, setSearchTerm] = useState('');
    const pageSize = 5;

    useEffect(() => {
        const fetchStatuses = async () => {
            const data = await getPlayerStatuses();
            setStatuses(data);
            setFilteredStatuses(data);
        };
        fetchStatuses();
    }, []);

    // Handle filtering by Player ID
    useEffect(() => {
        const filtered = statuses.filter(status =>
            status.playerId.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredStatuses(filtered);
        setCurrentPage(1);
    }, [searchTerm, statuses]);

    const handleSearchChange = (e) => {
        setSearchTerm(e.target.value);
    };

    // Paginate the filtered results
    const startIndex = (currentPage - 1) * pageSize;
    const currentStatuses = filteredStatuses.slice(startIndex, startIndex + pageSize);

    return (
        <div>
            <h1>Player Status Dashboard</h1>
            <input
                type="text"
                placeholder="Filter by Player ID"
                value={searchTerm}
                onChange={handleSearchChange}
            />
            <table>
                <thead>
                    <tr>
                        <th>Player ID</th>
                        <th>Status</th>
                        <th>Reason</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {currentStatuses.map((status, index) => (
                        <PlayerStatusRow
                            key={status.playerId}
                            status={status}
                            isReviewNeeded={index === 0 && status.playerId === 'Player123'}
                        />
                    ))}
                </tbody>
            </table>
            <Pagination
                totalItems={filteredStatuses.length}
                pageSize={pageSize}
                currentPage={currentPage}
                onPageChange={setCurrentPage}
            />
        </div>
    );
};

export default PlayerStatusTable;
