import React from 'react';
import styles from './CheckBox.module.css';

type CheckBoxProps = {
  isActive: boolean;
  text: string;
  onChange: () => void;
};

const CheckBox: React.FC<CheckBoxProps> = props => {
  return (
    <div className={styles.checkBoxBlock}>
      <input
        type={'checkbox'}
        checked={props.isActive}
        className={styles.checkBox}
        onChange={props.onChange}
      />
      <h6>{props.text}</h6>
    </div>
  );
};

export default CheckBox;
