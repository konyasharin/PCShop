import React from 'react';
import TCheckBox from 'types/TCheckBox.ts';
import styles from './Radio.module.css';
import createClassNames from 'utils/createClassNames.ts';

type RadioProps = TCheckBox & {
  onChange: () => void;
  className?: string;
};

const Radio: React.FC<RadioProps> = props => {
  return (
    <div className={createClassNames([styles.radioBlock, props.className])}>
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
