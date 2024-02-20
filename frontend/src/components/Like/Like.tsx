import useLike from '../../hooks/useLike.ts';
import heartActive from '../../assets/heart-active.png';
import heartDisabled from '../../assets/heart-disabled.png';
import styles from './Like.module.css';
import React from 'react';

type LikeProps = {
  className?: string;
  onClick?: () => void;
};

const Like: React.FC<LikeProps> = props => {
  const { likes, isActive, setIsActiveHandle } = useLike();
  return (
    <div className={`${styles.like} ${props.className}`}>
      <img
        src={isActive ? heartActive : heartDisabled}
        alt="like"
        onClick={() => {
          if (props.onClick) props.onClick();
          setIsActiveHandle(!isActive);
        }}
      />
      <span>{likes}</span>
    </div>
  );
};

export default Like;
