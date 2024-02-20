import styles from './TOPBuildsBlock.module.css';
import Like from 'components/Like/Like.tsx';

type TopProps = {
  PCImg: string;
  className?: string;
  prizeImg: string;
  name: string;
  description: string[];
};

function TOPBuildsBlock(props: TopProps) {
  const description = props.description.map((item, i) => {
    return <span key={i}>{item}</span>;
  });
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
        <img src={props.PCImg} alt={'sys block'} className={styles.sysblock} />
      </div>
      <div className={styles.mainText}>
        <div className={styles.title}>
          <h3 className={styles.name}>{props.name}</h3>
          <img src={props.prizeImg} alt={'prize'} className={styles.gprize} />
        </div>
        <div className={styles.description}>{description}</div>
      </div>
    </div>
  );
}

export default TOPBuildsBlock;
