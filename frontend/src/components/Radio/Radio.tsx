import React from 'react';
import TCheckBox from 'types/TCheckBox.ts';
import styles from './Radio.module.css';

const Radio: React.FC<TCheckBox & { onChange: () => void }> = props => {
  return (
    <div className={styles.radioBlock}>
      <input
        type="radio"
        checked={props.isActive}
        onChange={props.onChange}
        className={styles.radio}
      />
      <h6>{props.text}</h6>
    </div>
  );
};

export default Radio;
