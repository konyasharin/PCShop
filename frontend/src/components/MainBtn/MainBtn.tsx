import React from 'react';
import styles from './MainBtn.module.css';

type MainBtnProps = {
  children?: string;
};

const MainBtn: React.FC<MainBtnProps> = props => {
  return <button className={styles.mainBtn}>{props.children}</button>;
};

export default MainBtn;
