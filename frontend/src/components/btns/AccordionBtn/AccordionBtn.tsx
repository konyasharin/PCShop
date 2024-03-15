import React from 'react';
import createClassNames from 'utils/createClassNames.ts';
import styles from './AccordionBtn.module.css';
import arrowImg from 'assets/arrow.png';

type AccordionBtnProps = {
  text: string;
  isActive: boolean;
  className?: string;
  onClick?: () => void;
};

const AccordionBtn: React.FC<AccordionBtnProps> = props => {
  return (
    <button
      className={createClassNames([
        props.className,
        styles.btn,
        props.isActive ? styles.btnActive : styles.btnDisable,
      ])}
      onClick={props.onClick}
    >
      {props.text}
      <img src={arrowImg} alt="arrow" />
    </button>
  );
};

export default AccordionBtn;
