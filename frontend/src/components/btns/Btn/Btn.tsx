import React from 'react';
import styles from './Btn.module.css';
import createClassNames from 'utils/createClassNames.ts';

type PCBtnProps = {
  children?: string;
  className?: string;
  onClick?: () => void;
};

const Btn: React.FC<PCBtnProps> = props => {
  return (
    <button
      className={createClassNames([styles.pcBtn, props.className])}
      onClick={props.onClick}
    >
      {props.children}
    </button>
  );
};

export default Btn;
