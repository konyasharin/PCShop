package com.example.pc.Services;

import com.example.pc.IServices.IPowerUnitService;
import com.example.pc.Models.PowerUnitModel;
import com.example.pc.dto.PowerUnitDto;
import com.example.pc.repositories.PowerUnitRepo;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.util.List;

@Service
public class PowerUnitService implements IPowerUnitService {

    private final PowerUnitRepo powerUnitRepo;

    private static final Logger logger = LoggerFactory.getLogger(PowerUnitService.class);

    @Autowired
    public PowerUnitService(PowerUnitRepo powerUnitRepo) {
        this.powerUnitRepo = powerUnitRepo;
    }

    @Override
    public List<PowerUnitModel> getPowerUnits() {
        return powerUnitRepo.findAll();
    }

    @Override
    public PowerUnitModel getPowerUnitById(Long id) {
        return powerUnitRepo.findById(id).orElse(null);
    }

    @Override
    public PowerUnitModel updatePowerUnit(PowerUnitModel powerUnitModel) {
        return powerUnitRepo.save(powerUnitModel);
    }

    @Override
    public void deletePowerUnitById(Long id) {
        powerUnitRepo.deleteById(id);
    }

    @Override
    public PowerUnitModel createPowerUnit(PowerUnitDto powerUnitDto){

        try {
            byte[] imageData = powerUnitDto.getImage().getBytes();
            return powerUnitRepo.save(PowerUnitModel.builder()
                    .brand(powerUnitDto.getBrand())
                    .model(powerUnitDto.getModel())
                    .description(powerUnitDto.getDescription())
                    .price(powerUnitDto.getPrice())
                    .country(powerUnitDto.getCountry())
                    .battery((powerUnitDto.getBattery()))
                    .voltage(powerUnitDto.getVoltage())
                    .image(imageData)
                    .build());
        }catch(IOException e){

            logger.error("Failed to convert PowerUnit image to byte array", e);
            throw  new RuntimeException("Failed to convert PowerUnit image to byte array", e);
        }
    }
}
