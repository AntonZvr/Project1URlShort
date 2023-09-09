import React, { useState } from 'react';

export function LoginPage() {
    const [LoginUsername, setLoginUsername] = useState("");
    const [LoginPassword, setLoginPassword] = useState("");
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [role, setRole] = useState("");
    const [errorMessage, setErrorMessage] = useState("");
    const [errorRegMessage, setErrorRegMessage] = useState("");

    const handleLogin = async (e) => {
        e.preventDefault();
        const response = await fetch('https://localhost:44490/Users/authenticate', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },

            body: JSON.stringify({ firstName, lastName, LoginUsername, LoginPassword, role })
        });
        const data = await response.json();
        if (response.ok) {
            console.log("success log")
            setErrorMessage("");
            setLoginUsername("");
            setLoginPassword("");
        } else {
            console.log("error log")
            setErrorMessage("Username or password is incorrect");
        }
    };

    const handleRegister = async (e) => {
        e.preventDefault();
        const response = await fetch('https://localhost:44490/Users/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ firstName, lastName, username, password, role })
        });
        const data = await response.json();
        if (response.ok) {
            console.log("success reg")
            setErrorRegMessage("");
            setFirstName("");
            setLastName("");
            setUsername("");
            setPassword("");
            setRole("");
            setErrorRegMessage("")
        } else {
            console.log("error reg")
            setErrorRegMessage("Fill all fields");
        }
    };

    return (
        <div>
            <form onSubmit={handleLogin}>
                <h2>Login</h2>
                <input type="text" placeholder="Username" value={LoginUsername} onChange={e => setLoginUsername(e.target.value)} required />
                <input type="password" placeholder="Password" value={LoginPassword} onChange={e => setLoginPassword(e.target.value)} required />
                <button type="submit">Login</button>
                {errorMessage && <p>{errorMessage}</p>}
            </form>
            <form onSubmit={handleRegister}>
                <h2>Register</h2>
                <input type="text" placeholder="First Name" value={firstName} onChange={e => setFirstName(e.target.value)} required />
                <input type="text" placeholder="Last Name" value={lastName} onChange={e => setLastName(e.target.value)} required />
                <input type="text" placeholder="Username" value={username} onChange={e => setUsername(e.target.value)} required />
                <input type="password" placeholder="Password" value={password} onChange={e => setPassword(e.target.value)} required />
                <select value={role} onChange={e => setRole(e.target.value)} required>
                    <option value="">Select Role</option>
                    <option value="user">User</option>
                    <option value="admin">Admin</option>
                </select>
                <button type="submit">Register</button>
                {errorRegMessage && <p>{errorRegMessage}</p>}
            </form>
        </div>
    );
}
