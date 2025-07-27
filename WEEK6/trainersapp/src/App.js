// src/App.js
import React from 'react';
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import Home from './Components/Home';
import TrainerList from './Components/TrainerList';
import TrainerDetails from './Components/TrainerDetails';
import trainers from './TrainersMock';

function App() {
  return (
    <BrowserRouter>
      <div>
        <h1>TrainersApp</h1>
        <nav>
          <Link to="/">Home</Link> | <Link to="/trainers">Trainers</Link>
        </nav>
        <hr />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/trainers" element={<TrainerList trainers={trainers} />} />
          <Route path="/trainer/:id" element={<TrainerDetails />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
