package com.example.pc.controllers;

import com.example.pc.Models.PowerUnitModel;
import com.example.pc.Services.PowerUnitService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/api")
public class PowerUnitAPI {

    private static final Logger logger = LoggerFactory.getLogger(PowerUnitAPI.class);

    @Autowired
    private PowerUnitService powerUnitService;

    @PostMapping("/createComponent/PowerUnit")
    public ResponseEntity<Map<String, Object>> createPowerUnit(@RequestParam(name="brand") String brand,
                                                                 @RequestParam(name="mode") String model,
                                                                 @RequestParam(name="description") String description,
                                                                 @RequestParam(name="price") Integer price,
                                                                 @RequestParam(name="country") String country,
                                                                 @RequestParam(name="battery") String battery,
                                                                 @RequestParam(name="voltage") Integer  voltage,
                                                                 @RequestParam(name="image") MultipartFile image){

        try{

            if (image.isEmpty()) {
                logger.warn("No image provided");
                return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
            }

            PowerUnitModel powerUnitModel = new PowerUnitModel();
            powerUnitModel.setBrand(brand);
            powerUnitModel.setModel(model);
            powerUnitModel.setDescription(description);
            powerUnitModel.setPrice(price);
            powerUnitModel.setCountry(country);
            powerUnitModel.setBattery(battery);
            powerUnitModel.setVoltage(voltage);
            powerUnitModel.setImage(image.getBytes());

            PowerUnitModel savedPowerUnit = powerUnitService.addPowerUnit(powerUnitModel);

            Map<String, Object> PowerUnitResponse = new HashMap<>();
            PowerUnitResponse.put("type", "PowerUnit");
            PowerUnitResponse.put("id", savedPowerUnit.getId());
            PowerUnitResponse.put("ComponentData", savedPowerUnit);

            logger.info("PowerUnit created successfully: {}", savedPowerUnit);

            return new ResponseEntity<>(PowerUnitResponse, HttpStatus.CREATED);

        }catch(IOException e){
            e.printStackTrace();

            logger.error("Failed to create PowerUnit", e);

            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }

    }
}
