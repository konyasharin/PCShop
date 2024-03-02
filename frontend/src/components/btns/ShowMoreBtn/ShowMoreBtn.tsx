import React, { ReactNode } from 'react';
import createClassNames from 'utils/createClassNames.ts';
import styles from './ShowMoreBtn.module.css';

type ShowMoreBtnProps = {
  children?: ReactNode;
  className?: string;
  onClick?: () => void;
};

const ShowMoreBtn: React.FC<ShowMoreBtnProps> = props => {
  return (
    <button
      className={createClassNames([props.className, styles.btn])}
      onClick={props.onClick}
    >
      {props.children}
    </button>
  );
};

export default ShowMoreBtn;
