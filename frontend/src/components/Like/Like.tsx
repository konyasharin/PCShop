import useLike from '../../hooks/useLike.ts';
import heartActive from '../../assets/heart-active.png';
import heartDisabled from '../../assets/heart-disabled.png';
import styles from './Like.module.css';

function Like() {
  const { likes, isActive, setIsActiveHandle } = useLike();
  return (
    <div className={styles.like}>
      <img
        src={isActive ? heartActive : heartDisabled}
        alt="like"
        onClick={() => setIsActiveHandle(!isActive)}
      />
      <span>{likes}</span>
    </div>
  );
}

export default Like;
