import ChooseComponent from './ChooseComponent/ChooseComponent.tsx';
import videoCardIcon from 'assets/videocard-white-icon.png';
import processorIcon from 'assets/cpu-white-icon.png';
import styles from './ChooseComponents.module.css';
import EComponentTypes from 'enums/EComponentTypes.ts';

function ChooseComponents() {
  return (
    <section className={styles.blocks}>
      <ChooseComponent
        img={videoCardIcon}
        type={EComponentTypes.videoCard}
        title={'Видеокарта'}
        isImportant={true}
        errorType={'Success'}
        searchTitle={'Выберите видеокарту'}
      />
      <ChooseComponent
        img={processorIcon}
        type={EComponentTypes.processor}
        title={'Процессор'}
        isImportant={true}
        errorType={'Warning'}
        searchTitle={'Выберите процессор'}
      />
    </section>
  );
}

export default ChooseComponents;
