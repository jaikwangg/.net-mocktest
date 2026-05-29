import { useState, useEffect } from 'react';
import api from './api';
import { Student, StudentCreate } from './types';

function App() {
  const [students, setStudents] = useState<Student[]>([]);
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [isLoggedIn, setIsLoggedIn] = useState(!!localStorage.getItem('token'));
  const [newStudent, setNewStudent] = useState<StudentCreate>({ name: '', score: 0 });
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (isLoggedIn) {
      fetchStudents();
    }
  }, [isLoggedIn]);

  const fetchStudents = async () => {
    try {
      const response = await api.get<Student[]>('/students');
      setStudents(response.data);
      setError(null);
    } catch (err: any) {
      setError('Failed to fetch students. ' + (err.response?.data?.message || err.message));
    }
  };

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await api.post('/auth/login', { username, password });
      localStorage.setItem('token', response.data.token);
      setIsLoggedIn(true);
      setError(null);
    } catch (err: any) {
      setError('Login failed. Check credentials or validation rules.');
    }
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    setIsLoggedIn(false);
    setStudents([]);
  };

  const handleCreateStudent = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await api.post('/students', newStudent);
      fetchStudents();
      setNewStudent({ name: '', score: 0 });
      setError(null);
    } catch (err: any) {
      // Show validation errors from FluentValidation
      const valErrors = err.response?.data?.errors;
      if (valErrors) {
        setError(Object.values(valErrors).flat().join(', '));
      } else {
        setError(err.response?.data?.message || 'Failed to create student');
      }
    }
  };

  const handleDelete = async (id: number) => {
    try {
      await api.delete(`/students/${id}`);
      fetchStudents();
    } catch (err: any) {
      setError('Delete failed. Are you an Admin?');
    }
  };

  if (!isLoggedIn) {
    return (
      <div style={{ padding: '20px', maxWidth: '400px', margin: 'auto' }}>
        <h1>Login</h1>
        <form onSubmit={handleLogin}>
          <div>
            <label>Username (admin/user):</label><br/>
            <input value={username} onChange={e => setUsername(e.target.value)} style={{width: '100%'}} />
          </div>
          <div style={{ marginTop: '10px' }}>
            <label>Password (Admin123! / User123!):</label><br/>
            <input type="password" value={password} onChange={e => setPassword(e.target.value)} style={{width: '100%'}} />
          </div>
          <button type="submit" style={{ marginTop: '20px', width: '100%' }}>Login</button>
        </form>
        {error && <p style={{ color: 'red' }}>{error}</p>}
      </div>
    );
  }

  return (
    <div style={{ padding: '20px', maxWidth: '800px', margin: 'auto' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <h1>Student Management</h1>
        <button onClick={handleLogout}>Logout</button>
      </div>

      <div style={{ marginBottom: '30px', border: '1px solid #ccc', padding: '15px' }}>
        <h3>Add New Student</h3>
        <form onSubmit={handleCreateStudent} style={{ display: 'flex', gap: '10px' }}>
          <input 
            placeholder="Name" 
            value={newStudent.name} 
            onChange={e => setNewStudent({...newStudent, name: e.target.value})} 
          />
          <input 
            type="number" 
            placeholder="Score" 
            value={newStudent.score} 
            onChange={e => setNewStudent({...newStudent, score: Number(e.target.value)})} 
          />
          <button type="submit">Add</button>
        </form>
        {error && <p style={{ color: 'red' }}>{error}</p>}
      </div>

      <h3>Student List</h3>
      <table style={{ width: '100%', borderCollapse: 'collapse' }}>
        <thead>
          <tr style={{ textAlign: 'left', borderBottom: '2px solid #333' }}>
            <th>ID</th>
            <th>Name</th>
            <th>Score</th>
            <th>Grade</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {students.map(s => (
            <tr key={s.id} style={{ borderBottom: '1px solid #eee' }}>
              <td>{s.id}</td>
              <td>{s.name}</td>
              <td>{s.score}</td>
              <td style={{ fontWeight: 'bold' }}>{s.grade}</td>
              <td>
                <button onClick={() => handleDelete(s.id)} style={{ color: 'red' }}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;
