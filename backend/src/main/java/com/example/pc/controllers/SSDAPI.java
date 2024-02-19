package com.example.pc.controllers;

import com.example.pc.Models.SSDModel;
import com.example.pc.Services.SSDService;
import com.example.pc.dto.SsdDto;
import lombok.AllArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

@RestController
@AllArgsConstructor
public class SSDAPI {

    private static final Logger logger = LoggerFactory.getLogger(SSDAPI.class);

    @Autowired
    private SSDService ssdService;

    @PostMapping("/createComponent/SSD")
    public ResponseEntity<SSDModel> createSSD(@RequestBody SsdDto ssdDto){

        MultipartFile image = ssdDto.getImage();

        if (image == null || image.isEmpty()) {
            logger.warn("No image provided");
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        logger.info("SSD created successfully: {}", ssdDto);

        return new ResponseEntity<>(ssdService.createSSD(ssdDto),
                HttpStatus.CREATED);
    }
}
