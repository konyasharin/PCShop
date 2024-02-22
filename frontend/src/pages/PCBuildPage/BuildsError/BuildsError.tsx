import styles from './BuildsError.module.css';
import question from '../../../assets/Question.png';
import cross from '../../../assets/Cross.png';
import TBuildsErrors from 'types/TBuildsErrors.ts';
import React from 'react';

enum colors {
  red = '#FF0B0BB2',
  yellow = '#F7A400B2',
}

const BuildsError: React.FC<TBuildsErrors> = props => {
  const colorOfProblem = props.type === 'Warning' ? colors.yellow : colors.red;

  return (
    <div className={styles.body}>
      <div
        className={styles.rectangle}
        style={{ backgroundColor: colorOfProblem }}
      ></div>
      <div
        className={styles.circle}
        style={{ border: `4px solid ${colorOfProblem}` }}
      >
        <img
          src={props.type === 'Warning' ? question : cross}
          alt={'question'}
          className={styles.question}
          style={
            props.type === 'Warning' ? { height: '50px' } : { height: '38px' }
          }
        />
      </div>
      <div className={styles.text}>
        <h3>{props.title}</h3>
        <p>{props.description}</p>
      </div>
    </div>
  );
};

export default BuildsError;
