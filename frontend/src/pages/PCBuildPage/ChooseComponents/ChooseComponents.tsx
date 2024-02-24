import ChooseComponent from './ChooseComponent/ChooseComponent.tsx';
import videoCardIcon from 'assets/videocard-white-icon.png';
import processorIcon from 'assets/cpu-white-icon.png';
import styles from './ChooseComponents.module.css';

function ChooseComponents() {
  return (
    <section className={styles.blocks}>
      <ChooseComponent
        img={videoCardIcon}
        type={'videoCard'}
        title={'Видеокарта'}
        isImportant={true}
        errorType={'Success'}
        className={styles.block}
      />
      <ChooseComponent
        img={processorIcon}
        type={'processor'}
        title={'Процессор'}
        isImportant={true}
        errorType={'Warning'}
        className={styles.block}
      />
    </section>
  );
}

export default ChooseComponents;
