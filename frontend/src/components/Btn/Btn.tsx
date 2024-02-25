import React from 'react';
import styles from './Btn.module.css';
import createClassNames from "utils/createClassNames.ts";

type PCBtnProps = {
  children?: string;
  className?: string;
};

const Btn: React.FC<PCBtnProps> = props => {
  return <button className={createClassNames([styles.pcBtn, props.className])}>{props.children}</button>;
};

export default Btn;
