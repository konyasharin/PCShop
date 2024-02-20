import styles from './TOPBuildsBlock.module.css';
import Like from 'components/Like/Like.tsx';
import TBuildPreview from 'types/TBuildPreview.ts';

function TOPBuildsBlock(
  props: TBuildPreview & { prizeImg: string; className?: string },
) {
  return (
    <div className={`${styles.base} ${props.className}`}>
      <div className={styles.backgroundLight}></div>
      <div className={styles.backgroundDark}></div>
      <div className={styles.wrapper}>
        <Like
          className={styles.like}
          onClick={() => console.log('отключаем')}
        />
        <div className={styles.wrapperUpColor}></div>
        <img src={props.img} alt={'sys block'} className={styles.sysblock} />
      </div>
      <div className={styles.mainText}>
        <div className={styles.title}>
          <h3 className={styles.name}>{props.name}</h3>
          <img src={props.prizeImg} alt={'prize'} className={styles.gprize} />
        </div>
        <div className={styles.description}>
          <span>{props.description.videoCard}</span>
          <span>{props.description.processor}</span>
          <span>{props.description.RAM}</span>
          <span>{props.description.cooling}</span>
        </div>
      </div>
    </div>
  );
}

export default TOPBuildsBlock;
