import React from 'react';
import sysblock from '../../assets/sysblock.png';
import styles from './Top2.module.css';
import baccom from '../../assets/baccom.png';
import sprize from '../../assets/sprize.png';

type TopProps = {
  sysblockSrc?: string;
  baccomSrc?: string;
  sprizeSrc?: string;
  name?: string;
  description?: string[];
};

function Top2(props: TopProps) {
  const { sysblockSrc, baccomSrc, sprizeSrc, name, description } = props;

  return (
    <div className={styles.base}>
      <div className={styles.bacall}></div>
      <img src={sysblockSrc} alt={'sys block'} className={styles.sysblock} />
      <img src={baccomSrc} alt={'purple'} className={styles.baccom} />
      <div className={styles.mainText}>
        <div className={styles.par}>
          <h3 className={styles.name}>{name}</h3>
          <img src={sprizeSrc} alt={'prize'} className={styles.sprize} />
        </div>
        <div className={styles.description}>
          {description &&
            description.map((line, index) => (
              <React.Fragment key={index}>
                {line}
                <br />
              </React.Fragment>
            ))}
        </div>
      </div>
    </div>
  );
}

Top2.defaultProps = {
  sysblockSrc: sysblock,
  baccomSrc: baccom,
  sprizeSrc: sprize,
  name: 'КОЛИБРИ',
  description: [
    'RTX 4080 16GB',
    'Ryzen 7 4600',
    '32GB RAM',
    'Водяное охлаждение',
  ],
};

export default Top2;
