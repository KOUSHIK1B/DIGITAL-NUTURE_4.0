// src/Components/TrainerDetails.js
import React from 'react';
import { useParams } from 'react-router-dom';
import trainers from '../TrainersMock';

const TrainerDetails = () => {
  const { id } = useParams();
  const trainer = trainers.find((t) => t.trainerId === parseInt(id));

  if (!trainer) {
    return <p>Trainer not found.</p>;
  }

  return (
    <div>
      <h2>Trainer Details</h2>
      <p><strong>Name:</strong> {trainer.name}</p>
      <p><strong>Expertise:</strong> {trainer.expertise}</p>
      <p><strong>Experience:</strong> {trainer.experience} years</p>
    </div>
  );
};

export default TrainerDetails;
