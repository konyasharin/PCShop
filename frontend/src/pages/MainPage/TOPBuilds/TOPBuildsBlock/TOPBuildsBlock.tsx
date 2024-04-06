import styles from './TOPBuildsBlock.module.css';
import Like from 'components/Like/Like.tsx';
import TAssembly from 'types/TAssembly.ts';
import React from 'react';
import config from '../../../../../config.ts';
import useBuildCard from 'hooks/useBuildCard.ts';

type TOPBuildsBlockProps = {
  prizeImg: string;
  className?: string;
  assembly: TAssembly<string>;
};

const TOPBuildsBlock: React.FC<TOPBuildsBlockProps> = props => {
  const {
    videoCard,
    RAM,
    processor,
    cooler,
    likes,
    likeIsActive,
    setLikeIsActive,
  } = useBuildCard(props.assembly);

  return (
    <div className={`${styles.base} ${props.className}`}>
      <div className={styles.backgroundLight}></div>
      <div className={styles.backgroundDark}></div>
      <div className={styles.wrapper}>
        <Like
          className={styles.like}
          onClick={() => console.log('отключаем')}
          count={likes}
          isActive={likeIsActive}
          setIsActive={setLikeIsActive}
        />
        <div className={styles.wrapperUpColor}></div>
        <img
          src={`${config.backupUrl}/${props.assembly.image}`}
          alt={'sys block'}
          className={styles.img}
        />
      </div>
      <div className={styles.mainText}>
        <div className={styles.title}>
          <h3 className={styles.name}>{props.assembly.name}</h3>
          <img src={props.prizeImg} alt={'prize'} className={styles.gprize} />
        </div>
        <div className={styles.description}>
          <span>{videoCard ? videoCard.model : ''}</span>
          <span>{processor ? processor.model : ''}</span>
          <span>{RAM ? RAM.model : ''}</span>
          <span>{cooler ? cooler.model : ''}</span>
        </div>
      </div>
    </div>
  );
};

export default TOPBuildsBlock;
