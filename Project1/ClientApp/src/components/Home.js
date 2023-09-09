import React, { useState, useEffect } from 'react';

export function Home() {
    const [data, setData] = useState([]);
    const [url, setUrl] = useState('');
    const [isValidUrl, setIsValidUrl] = useState(true);
    const [showDuplicateMessage, setShowDuplicateMessage] = useState(false);

    useEffect(() => {
        getAllURLs();
    }, []);

    const getAllURLs = () => {
        fetch('https://localhost:44490/URLView/getAllURLs')
            .then(response => response.json())
            .then(data => setData(data))
            .catch(error => console.error(error));
    };

    const handleInputChange = (event) => {
        setUrl(event.target.value);
        setIsValidUrl(validateUrl(event.target.value));
    };

    const validateUrl = (value) => {
        const urlPattern = /^(https?:\/\/)?([\w-]+\.)+[\w-]+(\/[\w-./?%&=]*)?$/;
        return urlPattern.test(value);
    };

    const addNewURL = async () => {
        if (url.trim() !== '') {
            if (isValidUrl) {
                const newUrl = {
                    id: 0,
                    fullUrl: url,
                    shortUrl: ''
                };

                try {
                    const response = await fetch('https://localhost:44490/URLView/insertShortenedUrl', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(newUrl)
                    });

                    if (response.ok) {
                        const isDuplicate = data.some(item => item.fullUrl === url);
                        if (!isDuplicate) {
                            await getAllURLs();
                            setUrl('');
                            setShowDuplicateMessage(false);
                        } else {
                            setShowDuplicateMessage(true);
                        }
                    } else {
                        console.error('Failed to add URL');
                    }
                } catch (error) {
                    console.error(error);
                }
            }
        }
    };

    return (
        <div>
            <h1>Data Table</h1>
            <table>
                <thead>
                    <tr>
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
            {!isValidUrl && <p>Please enter a valid URL</p>}
            {showDuplicateMessage && <p>This URL already exists in the table</p>}
            <button onClick={addNewURL}>Add</button>
        </div>
    );
}
