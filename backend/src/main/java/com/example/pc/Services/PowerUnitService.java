package com.example.pc.Services;

import com.example.pc.Models.PowerUnitModel;
import com.example.pc.repositories.PowerUnitRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class PowerUnitService {

    private final PowerUnitRepo powerUnitRepo;

    @Autowired
    public PowerUnitService(PowerUnitRepo powerUnitRepo) {
        this.powerUnitRepo = powerUnitRepo;
    }

    public List<PowerUnitModel> getPowerUnits() {
        return powerUnitRepo.findAll();
    }

    public PowerUnitModel getPowerUnitById(Long id) {
        return powerUnitRepo.findById(id).orElse(null);
    }

    public PowerUnitModel savePowerUnit(PowerUnitModel powerUnitModel) {
        return powerUnitRepo.save(powerUnitModel);
    }

    public void deletePowerUnitById(Long id) {
        powerUnitRepo.deleteById(id);
    }
}
