import styles from './Top.module.css';

type TopProps = {
  sysblockSrc: string;
  className?: string;
  baccomSrc: string;
  prizeSrc: string;
  name: string;
  description: string[];
};

function Top(props: TopProps) {
  const description = props.description.map(item => {
    return <span>{item}</span>;
  });
  return (
    <div className={`${styles.base} ${props.className}`}>
      <div className={styles.bacall}></div>
      <div className={styles.wrapper}>
        <div className={styles.color}></div>
        <img
          src={props.sysblockSrc}
          alt={'sys block'}
          className={styles.sysblock}
        />
      </div>
      <img src={props.baccomSrc} alt={'purple'} className={styles.baccom} />
      <div className={styles.mainText}>
        <div className={styles.par}>
          <h3 className={styles.name}>{props.name}</h3>
          <img src={props.prizeSrc} alt={'prize'} className={styles.gprize} />
        </div>
        <div className={styles.description}>{description}</div>
      </div>
    </div>
  );
}

export default Top;
