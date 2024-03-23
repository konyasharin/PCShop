import React from 'react';
import styles from './MainBtn.module.css';

type MainBtnProps = {
  children?: string;
  className?: string;
  onClick?: () => void;
};

const MainBtn: React.FC<MainBtnProps> = props => {
  return (
    <button
      className={`${styles.mainBtn} ${props.className}`}
      onClick={props.onClick}
    >
      {props.children}
    </button>
  );
};

export default MainBtn;
