import heartActive from '../../assets/heart-active.png';
import heartDisabled from '../../assets/heart-disabled.png';
import styles from './Like.module.css';
import React from 'react';

type LikeProps = {
  className?: string;
  onClick?: () => void;
  count: number;
  isActive: boolean;
  setIsActive: (newIsActive: boolean) => void;
};

const Like: React.FC<LikeProps> = props => {
  return (
    <div className={`${styles.like} ${props.className}`}>
      <img
        src={props.isActive ? heartActive : heartDisabled}
        alt="like"
        onClick={() => {
          if (props.onClick) props.onClick();
          props.setIsActive(!props.isActive);
        }}
      />
      <span>{props.count}</span>
    </div>
  );
};

export default Like;
