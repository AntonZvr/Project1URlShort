import React, { useState, useEffect } from 'react';

export function Home() {
    const [data, setData] = useState([]);
    const [url, setUrl] = useState('');

    useEffect(() => {
        getAllURLs();
    }, []);

    const getAllURLs = () => {
        fetch('https://localhost:44490/URLView/getAllURLs')
            .then(response => response.json())
            .then(data => setData(data))
            .catch(error => console.error(error));
    };

    const addNewURL = () => {
        if (url.trim() !== '') {
            const newUrl = {
                id: 0,
                fullUrl: url,
                shortUrl: ''
            };

            fetch('https://localhost:44490/URLView/insertShortenedUrl', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newUrl)
            })
                .then(response => response.json())
                .then(() => {
                    getAllURLs(); // Fetch the updated data after successful insertion
                })
                .catch(error => console.error(error));

            setUrl(''); // Clear the input field after adding the new URL
        }
    };

    const handleInputChange = (event) => {
        setUrl(event.target.value);
    };

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
                            <td>{item.fullUrl}</td>
                            <td>https://localhost:44490/URLView/{item.shortUrl}</td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <h1>Insert URL</h1>
            <input
                type="text"
                value={url}
                onChange={handleInputChange}
            />
            <button onClick={() => {
                addNewURL();
                getAllURLs(); // Fetch the updated data on each button click
            }}>Add</button>
        </div>
    );
}
