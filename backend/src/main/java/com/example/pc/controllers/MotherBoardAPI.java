package com.example.pc.controllers;

import com.example.pc.Models.MotherBoardModel;
import com.example.pc.Services.MotherBoardService;
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
public class MotherBoardAPI {

    private static final Logger logger = LoggerFactory.getLogger(MotherBoardAPI.class);

    @Autowired
    private MotherBoardService motherBoardService;

    @PostMapping("/createComponent/MotherBoard")
    public ResponseEntity<Map<String, Object>> createMotherBoard(@RequestParam(name="brand") String brand,
                                                            @RequestParam(name="mode") String model,
                                                            @RequestParam(name="description") String description,
                                                            @RequestParam(name="price") Integer price,
                                                            @RequestParam(name="country") String country,
                                                            @RequestParam(name="frequency") Integer frequency,
                                                            @RequestParam(name="chipset") String  chipset,
                                                            @RequestParam(name="socket") String socket,
                                                            @RequestParam(name="image") MultipartFile image){

        try{

            if (image.isEmpty()) {
                logger.warn("No image provided");
                return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
            }

            MotherBoardModel motherBoardModel = new MotherBoardModel();
            motherBoardModel.setBrand(brand);
            motherBoardModel.setModel(model);
            motherBoardModel.setDescription(description);
            motherBoardModel.setPrice(price);
            motherBoardModel.setCountry(country);
            motherBoardModel.setFrequency(frequency);
            motherBoardModel.setChipset(chipset);
            motherBoardModel.setSocket(socket);
            motherBoardModel.setImage(image.getBytes());

            MotherBoardModel savedMotherBoard = motherBoardService.addMotherBoard(motherBoardModel);

            Map<String, Object> MotherBoardResponse = new HashMap<>();
            MotherBoardResponse.put("type", "MotherBoard");
            MotherBoardResponse.put("id", savedMotherBoard.getId());
            MotherBoardResponse.put("ComponentData", savedMotherBoard);


            logger.info("MotherBoard created successfully: {}", savedMotherBoard);

            return new ResponseEntity<>(MotherBoardResponse, HttpStatus.CREATED);

        }catch(IOException e){
            e.printStackTrace();

            logger.error("Failed to create MotherBoard:", e);

            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}
