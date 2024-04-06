import React from 'react';
import styles from './LastBuild.module.css';
import createClassNames from 'utils/createClassNames.ts';
import videoCardImg from 'assets/videocard-white-icon.png';
import processorImg from 'assets/cpu-white-icon.png';
import RAMImg from 'assets/ram-white-icon.png';
import coolerImg from 'assets/cooling-white-icon.png';
import Like from 'components/Like/Like.tsx';
import TAssembly from 'types/TAssembly.ts';
import useBuildCard from 'hooks/useBuildCard.ts';
import config from '../../../../../config.ts';

type LastBuildProps = {
  className?: string;
  assembly: TAssembly<string>;
};

const LastBuild: React.FC<LastBuildProps> = props => {
  const {
    videoCard,
    RAM,
    processor,
    cooler,
    likes,
    setLikeIsActive,
    likeIsActive,
  } = useBuildCard(props.assembly);
  return (
    <div className={createClassNames([props.className, styles.block])}>
      <div className={styles.mainImg}>
        <Like
          className={styles.like}
          count={likes}
          isActive={likeIsActive}
          setIsActive={setLikeIsActive}
        />
        <div className={styles.mainImgBackground}></div>
        <img
          src={`${config.backupUrl}/${props.assembly.image}`}
          alt="system block"
          className={styles.img}
        />
      </div>
      <div className={styles.downBlock}>
        <div className={styles.downBlockLight}></div>
        <div className={styles.downBlockDark}></div>
        <h3>{props.assembly.name}</h3>
        <div className={styles.descriptionBlock}>
          <img src={videoCardImg} alt="videoCard" />
          {videoCard ? videoCard.description : ''}
        </div>
        <div className={styles.descriptionBlock}>
          <img src={processorImg} alt="processor" />
          {processor ? processor.description : ''}
        </div>
        <div className={styles.descriptionBlock}>
          <img src={RAMImg} alt="RAM" />
          {RAM ? RAM.description : ''}
        </div>
        <div className={styles.descriptionBlock}>
          <img src={coolerImg} alt="cooling" />
          {cooler ? cooler.description : ''}
        </div>
      </div>
    </div>
  );
};

export default LastBuild;
