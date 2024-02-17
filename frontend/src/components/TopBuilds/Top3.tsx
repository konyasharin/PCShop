import React from 'react';
import sysblock from '../../assets/sysblock.png';
import styles from './Top3.module.css';
import baccom from '../../assets/baccom.png';
import bprize from '../../assets/bprize.png';

type TopProps = {
  sysblockSrc?: string;
  baccomSrc?: string;
  bprizeSrc?: string;
  name?: string;
  description?: string[];
};

function Top3(props: TopProps) {
  const { sysblockSrc, baccomSrc, bprizeSrc, name, description } = props;

  return (
    <div className={styles.base}>
      <div className={styles.bacall}></div>
      <img src={sysblockSrc} alt={'sys block'} className={styles.sysblock} />
      <img src={baccomSrc} alt={'purple'} className={styles.baccom} />
      <div className={styles.mainText}>
        <div className={styles.par}>
          <h3 className={styles.name}>{name}</h3>
          <img src={bprizeSrc} alt={'prize'} className={styles.bprize} />
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

Top3.defaultProps = {
  sysblockSrc: sysblock,
  baccomSrc: baccom,
  bprizeSrc: bprize,
  name: 'КОЛИБРИ',
  description: [
    'RTX 4080 16GB',
    'Ryzen 7 4600',
    '32GB RAM',
    'Водяное охлаждение',
  ],
};

export default Top3;
