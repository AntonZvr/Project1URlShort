import React, { useState, useEffect } from 'react';

export function Home() {
    const [data, setData] = useState([]);

    useEffect(() => {
        fetch('https://localhost:44490/URLView/getAllURLs')
            .then(response => response.json())
            .then(data => setData(data));
    }, []);

    return (
        <div>
            <h1>Data Table</h1>
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Full URL</th>
                        <th>Short URL</th>
                    </tr>
                </thead>
                <tbody>
                    {data.map(item => (
                        <tr key={item.id}>
                            <td>{item.id}</td>
                            <td>{item.fullUrl}</td>
                            <td>{item.shortUrl}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

