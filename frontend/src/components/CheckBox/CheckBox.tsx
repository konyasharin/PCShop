import React from 'react';
import styles from './CheckBox.module.css';
import TCheckBox from 'types/TCheckBox.ts';
import createClassNames from 'utils/createClassNames.ts';

type CheckBoxProps = TCheckBox & {
  onChange: () => void;
  className?: string;
};

const CheckBox: React.FC<CheckBoxProps> = props => {
  return (
    <div className={createClassNames([styles.checkBoxBlock, props.className])}>
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
