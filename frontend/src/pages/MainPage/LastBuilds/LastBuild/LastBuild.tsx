import React from 'react';
import TBuildPreview from 'types/TBuildPreview.ts';
import styles from './LastBuild.module.css';
import createClassNames from 'utils/createClassNames.ts';
import videoCard from 'assets/videocard-white-icon.png';
import processor from 'assets/cpu-white-icon.png';
import RAM from 'assets/ram-white-icon.png';
import cooling from 'assets/cooling-white-icon.png';
import Like from 'components/Like/Like.tsx';

const LastBuild: React.FC<TBuildPreview> = props => {
  return (
    <div className={createClassNames([props.className, styles.block])}>
      <div className={styles.mainImg}>
        <Like className={styles.like} />
        <div className={styles.mainImgBackground}></div>
        <img src={props.img} alt="system block" />
      </div>
      <div className={styles.downBlock}>
        <div className={styles.downBlockLight}></div>
        <div className={styles.downBlockDark}></div>
        <h3>{props.name}</h3>
        <div className={styles.descriptionBlock}>
          <img src={videoCard} alt="videoCard" />
          {props.description.videoCard}
        </div>
        <div className={styles.descriptionBlock}>
          <img src={processor} alt="processor" />
          {props.description.processor}
        </div>
        <div className={styles.descriptionBlock}>
          <img src={RAM} alt="RAM" />
          {props.description.RAM}
        </div>
        <div className={styles.descriptionBlock}>
          <img src={cooling} alt="cooling" />
          {props.description.cooling}
        </div>
      </div>
    </div>
  );
};

export default LastBuild;
