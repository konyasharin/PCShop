import React from 'react';
import styles from './StorageAdd.module.css';

type StorageAddProps = {
  setCount: React.Dispatch<number>;
  count: number;
};

const StorageAdd: React.FC<StorageAddProps> = props => {
  return (
    <div>
      <button
        className={styles.btn}
        onClick={() => props.setCount(props.count - 1)}
      >
        -1
      </button>
      <button
        className={styles.btn}
        onClick={() => props.setCount(props.count - 10)}
      >
        -10
      </button>
      <button
        className={styles.btn}
        onClick={() => props.setCount(props.count + 1)}
      >
        +1
      </button>
      <button
        className={styles.btn}
        onClick={() => props.setCount(props.count + 10)}
      >
        +10
      </button>
    </div>
  );
};

export default StorageAdd;
