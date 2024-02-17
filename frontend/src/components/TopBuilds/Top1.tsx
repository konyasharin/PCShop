import React from 'react';
import sysblock from '../../assets/sysblock.png';
import styles from './Top1.module.css';
import baccom from '../../assets/baccom.png';
import gprize from '../../assets/gprize.png';

type TopProps = {
  sysblockSrc?: string;
  baccomSrc?: string;
  gprizeSrc?: string;
  name?: string;
  description?: string[];
};

function Top1(props: TopProps) {
  const { sysblockSrc, baccomSrc, gprizeSrc, name, description } = props;

  return (
    <div className={styles.base}>
      <div className={styles.bacall}></div>
      <img src={sysblockSrc} alt={'sys block'} className={styles.sysblock} />
      <img src={baccomSrc} alt={'purple'} className={styles.baccom} />
      <div className={styles.mainText}>
        <div className={styles.par}>
          <h3 className={styles.name}>{name}</h3>
          <img src={gprizeSrc} alt={'prize'} className={styles.gprize} />
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

Top1.defaultProps = {
  sysblockSrc: sysblock,
  baccomSrc: baccom,
  gprizeSrc: gprize,
  name: 'КОЛИБРИ',
  description: [
    'RTX 4080 16GB',
    'Ryzen 7 4600',
    '32GB RAM',
    'Водяное охлаждение',
  ],
};

export default Top1;
