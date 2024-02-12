import React from 'react';
import styles from './MainBtn.module.css';

type MainBtnProps = {
  children?: string;
  className?: string;
};

const MainBtn: React.FC<MainBtnProps> = props => {
  return (
    <button className={`${styles.mainBtn} ${props.className}`}>
      {props.children}
    </button>
  );
};

export default MainBtn;
