import React from 'react';

const Pagination = ({ totalItems, pageSize, currentPage, onPageChange }) => {
    const totalPages = Math.ceil(totalItems / pageSize);

    if (totalPages === 1) return null;

    const pages = Array.from({ length: totalPages }, (_, i) => i + 1);

    return (
        <div className="pagination">
            {pages.map(page => (
                <button
                    key={page}
                    className={page === currentPage ? 'active' : ''}
                    onClick={() => onPageChange(page)}
                >
                    {page}
                </button>
            ))}
        </div>
    );
};

export default Pagination;
