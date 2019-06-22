#!/usr/bin/python3
# -*- coding: utf-8 -*-
# Jheng-Hong Wang, 2019
# Email: worker2110@gmail.com

# import packages
import sys
import requests
import urllib.request
import json
from random import randint

from PyQt5.QtCore import Qt
from PyQt5.QtGui import QFont, QPalette
from PyQt5.QtWidgets import (
    QApplication, QWidget, QDesktopWidget, QToolTip,
    QLabel, QLineEdit, QComboBox, QPushButton, QMessageBox,
    QGridLayout, QHBoxLayout, QVBoxLayout
)
# ==========================================

class Window_Setter(QWidget):
    def __init__(self, parent=None, flags=Qt.WindowFlags()):
        """
        Initiating window UI configure
        
        Arguments:
            QWidget {[type]} -- [description]
        
        Keyword Arguments:
            parent {[type]} -- [description] (default: {None})
            flags {[type]} -- [description] (default: {Qt.WindowFlags()})
        """
        super().__init__(parent=parent, flags=flags)

        self.initUI()   # Initiating UI

    def __del__(self):
        pass

    def initUI(self):
        """
        Initiating UI configure
        """
        self.width_size = 500   # window width size
        self.height_size = 170  # window height size
        self.setFixedSize(self.width_size, self.height_size)    # lock size
        self.center()   # Setting window to center

        QToolTip.setFont(QFont("SansSerif", 16))    # 設定提示字字型為大小14的宋體

        self.initLayout()   # Setting Layout

    def center(self):
        """
        Making window center in the screen
        """
        qr = self.frameGeometry()
        cp = QDesktopWidget().availableGeometry().center()
        qr.moveCenter(cp)
        self.move(qr.topLeft())

    def initLayout(self):
        main_layout = QVBoxLayout(self)
        main_layout.setStretch(5, 1)

        # Setting space layout
        space_layout = QHBoxLayout()
        space_layout.setStretch(1, 1)

        # Add children layout to main_layout
        main_layout.addLayout(self.title_layout(), stretch=0)
        main_layout.addLayout(self.options_layout(), stretch=1)
        main_layout.addLayout(self.result_layout(), stretch=2)
        main_layout.addLayout(self.buttons_layout(), stretch=3)
        main_layout.addLayout(space_layout, stretch=4)

    def title_layout(self):
        title = QLabel('隨機抽選餐廳程式')
        title.setAlignment(Qt.AlignHCenter)     # Setting alignment
        title.setFont(QFont("SansSerif", 16))   # Setting font style

        # Setting Title layout
        sub_layout = QHBoxLayout()
        sub_layout.setStretch(1, 1)
        sub_layout.addWidget(title, stretch=0)

        return sub_layout

    def options_layout(self):
        options_label = QLabel("餐聽種類: ")
        options_label.setAlignment(Qt.AlignLeft)        # Setting alignment
        options_label.setFont(QFont("SansSerif", 16))   # Setting font style

        options = QComboBox()
        options.addItem("All")  # Defualt Value
        # Loading data from my api
        with urllib.request.urlopen("https://{hostname}/api/genres") as url:
            data = json.loads(url.read().decode())
            for i in data:
                options.addItem(i)
        
        options.setFont(QFont("SansSerif", 16)) # Setting font style

        self.genre_text = "All" # Setting default value
        options.activated[str].connect(self.onActivated) # Add listener

        # Setting Selecter layout
        sub_layout = QHBoxLayout()
        sub_layout.setStretch(1, 2)
        sub_layout.addWidget(options_label, stretch=0)
        sub_layout.addWidget(options, stretch=1)

        return sub_layout

    def result_layout(self):
        result_lable = QLabel("抽選結果: ")
        result_lable.setAlignment(Qt.AlignLeft)         # Setting alignment
        result_lable.setFont(QFont("SansSerif", 16))    # Setting font style

        self.result = QLineEdit()
        self.result.setAlignment(Qt.AlignLeft)          # Setting alignment
        self.result.setFont(QFont("SansSerif", 16))     # Setting font style

        # Setting result layout
        sub_layout = QHBoxLayout()
        sub_layout.setStretch(1, 2)
        sub_layout.addWidget(result_lable, stretch=0)
        sub_layout.addWidget(self.result, stretch=1)

        return sub_layout

    def buttons_layout(self):
        PrizeDrawingButton = QPushButton("隨機抽選")
        PrizeDrawingButton.setMinimumSize(100, 40)                  # Setting min size
        PrizeDrawingButton.clicked.connect(self.control_with_api)   # Add listener
        PrizeDrawingButton.setFont(QFont("SansSerif", 16))          # Setting font style
        PrizeDrawingButton.setToolTip("This is <b>Prize Drawing Button</b>.")

        CloseButton = QPushButton("離開")
        CloseButton.setMinimumSize(100, 40)                         # Setting min size
        CloseButton.clicked.connect(self.close)                     # Add listener
        PrizeDrawingButton.setFont(QFont("SansSerif", 16))          # Setting font style
        CloseButton.setToolTip("This is <b>Quit Button</b>.")

        # Setting buttons layout
        sub_layout = QHBoxLayout()
        sub_layout.setStretch(1, 2)
        sub_layout.addWidget(PrizeDrawingButton, stretch=1)
        sub_layout.addWidget(CloseButton, stretch=1)

        return sub_layout

    def onActivated(self, text):
        self.genre_text = text

    def control_with_api(self):
        """
        Loading data from my api to control
        """
        if self.genre_text == "All":
            with urllib.request.urlopen("https://{hostname}/api/") as url:
                data = json.loads(url.read().decode())
                self.result.setText(data[randint(0, (len(data)-1))]["name"])
        else:
            url_text = "https://{hostname}/api/genre/"+self.genre_text
            req = requests.request("GET", url_text)
            data = req.json()
            self.result.setText(data[randint(0, (len(data)-1))]["name"])

    def closeEvent(self, event):
        """
        Close warning window
        
        Arguments:
            event {[type]} -- [description]
        """
        reply = QMessageBox.question(
            self,
            'Warning',
            "Are you sure to quit?",
            QMessageBox.Yes | QMessageBox.No,
            QMessageBox.No
        )

        if reply == QMessageBox.Yes:
            event.accept()
        else:
            event.ignore()

def main():
    app = QApplication(sys.argv)                # Initiating app
    app.setApplicationName("今天吃什麼")         # Setting app name
    app.setApplicationDisplayName("今天吃什麼")  # Setting app window display name
    app.setStyle("Fusion")                      # Setting style
    
    window = Window_Setter()    # Initiating window
    window.show()               # Display window

    sys.exit(app.exec_())   # Exit application

if __name__ == '__main__':
    main()
    