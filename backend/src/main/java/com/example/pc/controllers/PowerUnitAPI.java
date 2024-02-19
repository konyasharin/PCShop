package com.example.pc.controllers;

import com.example.pc.Models.PowerUnitModel;
import com.example.pc.Services.PowerUnitService;
import com.example.pc.dto.PowerUnitDto;
import lombok.AllArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

@RestController
@AllArgsConstructor
public class PowerUnitAPI {

    private static final Logger logger = LoggerFactory.getLogger(PowerUnitAPI.class);

    @Autowired
    private PowerUnitService powerUnitService;

    @PostMapping("/createComponent/PowerUnit")
    public ResponseEntity<PowerUnitModel> createPowerUnit(@RequestBody PowerUnitDto powerUnitDto){

        MultipartFile image = powerUnitDto.getImage();

        if (image == null || image.isEmpty()) {
            logger.warn("No image provided");
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        logger.info("PowerUnit created successfully: {}", powerUnitDto);

        return new ResponseEntity<>(powerUnitService.createPowerUnit(powerUnitDto),
                HttpStatus.CREATED);

    }
}
