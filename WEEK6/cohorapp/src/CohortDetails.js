import React from 'react';
import styles from './CohortDetails.module.css'; // Import CSS module

class CohortDetails extends React.Component {
  render() {
    const cohorts = [
      { id: 1, name: "React April 2024", status: "ongoing" },
      { id: 2, name: "Java March 2024", status: "completed" },
      { id: 3, name: "Python Feb 2024", status: "ongoing" }
    ];

    return (
      <div>
        <h2>All Cohorts</h2>
        {cohorts.map(cohort => (
          <div key={cohort.id} className={styles.box}>
            <h3 style={{ color: cohort.status === "ongoing" ? "green" : "blue" }}>
              {cohort.name}
            </h3>
            <dl>
              <dt>Status:</dt>
              <dd>{cohort.status}</dd>
            </dl>
          </div>
        ))}
      </div>
    );
  }
}

export default CohortDetails;
