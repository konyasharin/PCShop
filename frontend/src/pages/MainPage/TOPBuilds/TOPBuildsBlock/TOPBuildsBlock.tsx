import styles from './TOPBuildsBlock.module.css';
import Like from 'components/Like/Like.tsx';
import TAssembly from 'types/TAssembly.ts';
import React, { useEffect, useState } from 'react';
import getComponent from 'api/components/getComponent.ts';
import TOneOfComponents from 'types/components/TOneOfComponents.ts';
import config from '../../../../../config.ts';
import useLike from 'hooks/useLike.ts';

type TOPBuildsBlockProps = {
  prizeImg: string;
  className?: string;
  assembly: TAssembly<string>;
};

const TOPBuildsBlock: React.FC<TOPBuildsBlockProps> = props => {
  const [videoCard, setVideoCard] = useState<TOneOfComponents<string> | null>(
    null,
  );
  const [processor, setProcessor] = useState<TOneOfComponents<string> | null>(
    null,
  );
  const [RAM, setRAM] = useState<TOneOfComponents<string> | null>(null);
  const [cooler, setCooler] = useState<TOneOfComponents<string> | null>(null);
  const { likes, isActive, setIsActiveHandle } = useLike(props.assembly.likes);
  useEffect(() => {
    async function getMainComponents() {
      setVideoCard(
        (await getComponent('videoCard', props.assembly.videoCardId)).data,
      );
      setProcessor(
        (await getComponent('processor', props.assembly.processorId)).data,
      );
      setRAM((await getComponent('RAM', props.assembly.ramId)).data);
      setCooler((await getComponent('cooler', props.assembly.coolerId)).data);
    }
    void getMainComponents();
  }, []);
  return (
    <div className={`${styles.base} ${props.className}`}>
      <div className={styles.backgroundLight}></div>
      <div className={styles.backgroundDark}></div>
      <div className={styles.wrapper}>
        <Like
          className={styles.like}
          onClick={() => console.log('отключаем')}
          count={likes}
          isActive={isActive}
          setIsActive={setIsActiveHandle}
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
